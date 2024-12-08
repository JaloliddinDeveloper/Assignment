namespace Assignment.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender GenderType {get; set; }
        public Language Languages { get; set; }

        public string StudentPicture {  get; set; } 
    }
}
