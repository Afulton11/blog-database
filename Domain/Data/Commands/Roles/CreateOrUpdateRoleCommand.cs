namespace Domain.Data.Commands.Roles
{
    public class CreateOrUpdateRoleCommand : ICommand
    {
        public int? RoleId { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
