using case_study_backend.Model;
using case_study_backend.Repository.Interface;
using Dapper;

namespace case_study_backend.Repository;

public class StoryDetailRepository : GenericRepository<StoryDetail>, IStoryDetailRepository
{
    public StoryDetailRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SelectString =>
        @"SELECT * FROM dbo.StoryDetail";

    protected override string InsertString =>
        @"INSERT INTO dbo.StoryDetail (Id,StoryId, ImagePath, IsVideo, Duration, CreatedAt, CreatedBy,IsInUse)
VALUES (@Id,@StoryId, @ImagePath, @IsVideo, @Duration, @CreatedAt, @CreatedBy,@IsInUse) 
SELECT IDENT_CURRENT('dbo.StoryDetail')";

    protected override string UpdateString =>
        @"UPDATE dbo.StoryDetail
SET  ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, IsInUse = @IsInUse 
WHERE Id = @Id";

    protected override string DeleteString => @"DELETE FROM dbo.StoryDetail WHERE Id = @Id";

    protected override DynamicParameters InsertCommandParametersAdd(StoryDetail entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@StoryId", entity.StoryId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@ImagePath", entity.ImagePath, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        parameters.Add("@IsVideo", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        parameters.Add("@Duration", entity.Duration, System.Data.DbType.Int32,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedAt", entity.CreatedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedBy", entity.CreatedBy, System.Data.DbType.String, System.Data.ParameterDirection.Input,
            128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        return parameters;
    }

    protected override DynamicParameters UpdateCommandParametersAdd(StoryDetail entity)
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