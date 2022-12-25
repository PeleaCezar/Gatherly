using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public sealed class Role : Enumeration<Role>
    {
        public static readonly Role Registered = new(1, "Registered");

        public Role(int id, string name)
            : base(id, name)
        {
        }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
