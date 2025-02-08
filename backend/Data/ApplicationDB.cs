using Microsoft.EntityFrameworkCore;
using Models.UserModel;
namespace Data.ApplicationDB;

public class ApplicationDB : DbContext {
    public ApplicationDB(DbContextOptions<ApplicationDB> dbContextOptions): base(dbContextOptions) {

    }

    public DbSet<UserModel> UserModel {get; set;}
}