using case_study_backend.Model;
using case_study_backend.Repository.Interface;
using Dapper;

namespace case_study_backend.Repository;

public class RelationsRepository : GenericRepository<Relations>, IRelationsRepository
{
    public RelationsRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SelectString =>
        @"SELECT * FROM dbo.Relations";

    protected override string InsertString =>
        @"INSERT INTO dbo.Relations (Id,UserId,UserInRelationshipId, CreatedAt, CreatedBy,IsInUse)
VALUES (@Id,@UserId, @UserInRelationshipId, @CreatedAt, @CreatedBy,@IsInUse) 
SELECT IDENT_CURRENT('dbo.Relations')";

    protected override string UpdateString =>
        @"UPDATE dbo.Relations
SET  ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, IsInUse = @IsInUse 
WHERE Id = @Id";

    protected override string DeleteString => @"DELETE FROM dbo.Relations WHERE Id = @Id";

    protected override DynamicParameters InsertCommandParametersAdd(Relations entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@UserId", entity.UserId, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@UserInRelationshipId", entity.UserInRelationshipId, System.Data.DbType.Guid,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedAt", entity.CreatedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedBy", entity.CreatedBy, System.Data.DbType.String, System.Data.ParameterDirection.Input,
            128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        return parameters;
    }

    protected override DynamicParameters UpdateCommandParametersAdd(Relations entity)
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