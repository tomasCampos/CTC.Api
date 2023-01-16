namespace CTC.Api.Features.User.Contracts
{
    public sealed class RegisterUserRequest
    {
        public string UserFirstName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserDocument { get; set; }
        public string UserLastName { get; set; }
        public int UserPermission { get; set; } //transformar em enum
        public string UserPassword { get; set; }

        //Creação de usuário no firebase
        //https://www.youtube.com/watch?v=jkTaHb0M4nw&t=247s
    }
}
