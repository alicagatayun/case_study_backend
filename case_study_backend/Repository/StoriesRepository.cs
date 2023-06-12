using System.Transactions;
using case_study_backend.Model;
using case_study_backend.Model.dto;
using case_study_backend.Repository.Interface;
using case_study_backend.Result;
using Dapper;
using Microsoft.Data.SqlClient;

namespace case_study_backend.Repository;

public class StoriesRepository : GenericRepository<Stories>, IStoriesRepository
{
    public StoriesRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SelectString =>
        @"SELECT * FROM dbo.Stories";

    protected override string InsertString =>
        @"INSERT INTO dbo.Stories (Id, UserId, CreatedAt, CreatedBy,IsInUse)
VALUES (@Id,@UserId, @CreatedAt, @CreatedBy,@IsInUse) 
SELECT IDENT_CURRENT('dbo.Stories')";

    protected override string UpdateString =>
        @"UPDATE dbo.Stories
SET  ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, IsInUse = @IsInUse
WHERE Id = @Id";

    protected override string DeleteString => @"DELETE FROM dbo.Relations WHERE Id = @Id";

    protected override DynamicParameters InsertCommandParametersAdd(Stories entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@UserId", entity.UserId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

        parameters.Add("@CreatedAt", entity.CreatedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedBy", entity.CreatedBy, System.Data.DbType.String, System.Data.ParameterDirection.Input,
            128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        return parameters;
    }

    protected override DynamicParameters UpdateCommandParametersAdd(Stories entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@ModifiedAt", entity.ModifiedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@ModifiedBy", entity.ModifiedBy, System.Data.DbType.String,
            System.Data.ParameterDirection.Input, 128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

        return parameters;
    }

    public async Task<EndpointResult<List<UserDto>>> GetUserStories(Guid userId)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            if (Configuration == null)
                return new EndpointResult<List<UserDto>>
                {
                    Data = null,
                    ErrorMessage = "No configuration found",
                    HasError = true
                };
            await using var sqlConnection =
                new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));


            var query = $"Select * from Relations where UserId = '{userId}'";
            var userRelations = await sqlConnection.QueryAsync<Relations>(query);
            List<UserDto> users = new List<UserDto>();

            foreach (var elem in userRelations)
            {
                var userDto = new UserDto();
                query = $"Select * from Users where Id = '{elem.UserInRelationshipId}'";
                var user = await
                    sqlConnection.QueryFirstAsync<User>(query);
                userDto.user = user;

                query = "Select * from Stories where " +
                        $"UserId ='{elem.UserInRelationshipId}' AND IsInUse = 1";
                var userStories =
                    await sqlConnection.QuerySingleOrDefaultAsync<Stories>(query);


                userDto.story = userStories;
                if (userDto.story != null)
                {
                    query = "SELECT * from StorySeen AS SS,Stories " +
                            "AS S,StoryDetail AS SD  where " +
                            "S.Id = SD.StoryId AND " +
                            "SD.Id = SS.StoryDetailId AND" +
                            $" SS.UserId = '{userId}' AND" +
                            $" SD.StoryId ='{userStories.Id}'";

                    var storySeenEnumerable =
                        await sqlConnection.QueryAsync<StorySeen>(query);
                    var seenEnumerable = storySeenEnumerable as StorySeen[] ?? storySeenEnumerable.ToArray();
                    if (seenEnumerable.ToList().Count == userStories.NumberOfStoryGroup)
                    {
                        userDto.AllSeen = true;
                    }
                }

                users.Add(userDto);
            }

            scope.Complete();
            return new EndpointResult<List<UserDto>>
            {
                Data = users,
                HasError = false
            };
        }
        catch (Exception e)
        {
            return new EndpointResult<List<UserDto>>
            {
                Data = null,
                ErrorMessage = e.ToString(),
                HasError = true
            };
        }
    }
}