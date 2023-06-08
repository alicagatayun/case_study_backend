using case_study_backend.Model;
using case_study_backend.Repository.Interface;
using Dapper;

namespace case_study_backend.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SelectString =>
        @"SELECT * FROM dbo.Users";

    protected override string InsertString =>
        @"INSERT INTO dbo.Users (Id,Name, ProfilePhotoPath, CreatedAt, CreatedBy,IsInUse)
VALUES (@Id,@Name, @ProfilePhotoPath, @CreatedAt, @CreatedBy,@IsInUse) 
SELECT IDENT_CURRENT('dbo.Users')";

    protected override string UpdateString =>
        @"UPDATE dbo.Users
SET Name = @Name, ProfilePhotoPath = @ProfilePhotoPath, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy
WHERE Id = @Id";

    protected override string DeleteString => @"DELETE FROM dbo.Users WHERE Id = @Id";

    protected override DynamicParameters InsertCommandParametersAdd(User entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@Name", entity.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        parameters.Add("@ProfilePhotoPath", entity.ProfilePhotoPath, System.Data.DbType.String,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedAt", entity.CreatedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@CreatedBy", entity.CreatedBy, System.Data.DbType.String, System.Data.ParameterDirection.Input,
            128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
        return parameters;
    }

    protected override DynamicParameters UpdateCommandParametersAdd(User entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", entity.Id, System.Data.DbType.Guid, System.Data.ParameterDirection.Input);
        parameters.Add("@Name", entity.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        parameters.Add("@ProfilePhotoPath", entity.ProfilePhotoPath, System.Data.DbType.String,
            System.Data.ParameterDirection.Input);
        parameters.Add("@ModifiedAt", entity.ModifiedAt, System.Data.DbType.DateTime,
            System.Data.ParameterDirection.Input);
        parameters.Add("@ModifiedBy", entity.ModifiedBy, System.Data.DbType.String,
            System.Data.ParameterDirection.Input, 128);
        parameters.Add("@IsInUse", entity.IsInUse, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

        return parameters;
    }
}