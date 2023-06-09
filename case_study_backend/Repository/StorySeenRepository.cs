using case_study_backend.Model;
using case_study_backend.Repository.Interface;
using Dapper;

namespace case_study_backend.Repository;

public class StorySeenRepository : GenericRepository<StorySeen>, IStorySeenRepository
{
    public StorySeenRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SelectString =>
        @"SELECT * FROM dbo.StorySeen";

    protected override string InsertString =>
        @"INSERT INTO dbo.StorySeen (Id, StoryDetailId, UserId, CreatedAt, CreatedBy,IsInUse)
VALUES (@Id,@StoryDetailId, @UserId, @CreatedAt, @CreatedBy,@IsInUse) 
SELECT IDENT_CURRENT('dbo.StorySeen')";

    protected override string UpdateString =>
        @"UPDATE dbo.StorySeen
SET  ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, IsInUse = @IsInUse 
WHERE Id = @Id";

    protected override string DeleteString => @"DELETE FROM dbo.StorySeen WHERE Id = @Id";

    protected override DynamicParameters InsertCommandParametersAdd(StorySeen entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@StoryDetailId", entity.StoryDetailId, System.Data.DbType.Guid,
            System.Data.ParameterDirection.Input);
        parameters.Add("@UserId", entity.UserId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);

        parameters.Add("@CreatedAt", entity.CreatedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedBy", entity.CreatedBy, System.Data.DbType.String, System.Data.ParameterDirection.Input,
            128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        return parameters;
    }

    protected override DynamicParameters UpdateCommandParametersAdd(StorySeen entity)
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
}