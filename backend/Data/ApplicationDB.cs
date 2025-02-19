using Microsoft.EntityFrameworkCore;
using Models.UserModel;
namespace Data.ApplicationDB;

public class ApplicationDB : DbContext {
    public ApplicationDB(DbContextOptions<ApplicationDB> dbContextOptions): base(dbContextOptions) {

    }

    public DbSet<UserModel> UserModel {get; set;}
    public DbSet<DoctorModel> Doctors {get; set;}
    public DbSet<Patient> Patient {get; set;}
}