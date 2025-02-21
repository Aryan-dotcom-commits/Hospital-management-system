using Models.Enums;
using Models;

public class RegisterModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public bool IsPatient { get; set; }
    public bool IsDoctor { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public Diagnosis Diagnosis { get; set; }
}
