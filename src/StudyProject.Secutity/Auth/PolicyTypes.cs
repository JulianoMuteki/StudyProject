using System.Security.Claims;

namespace StudyProject.Secutity.Auth
{
    public static class PolicyTypes
    {
        public const string NAME = "default.policy";

        public static IList<string> ListAllRoles
        {
            get
            {
                var roles = new List<string>();
                foreach (var role in Enum.GetNames(typeof(RoleAuthorize)))
                {
                    var result = (RoleAuthorize)Enum.Parse(typeof(RoleAuthorize), role);
                    roles.Add(role);
                }
                return roles;
            }
        }

        public static IDictionary<PERMISSIONS, Claim> ListAllClaims
        {
            get
            {
                var claims = new Dictionary<PERMISSIONS, Claim>();
                foreach (var permission in Enum.GetNames(typeof(PERMISSIONS)))
                {
                    var result = (PERMISSIONS)Enum.Parse(typeof(PERMISSIONS), permission);
                    claims.Add(result, new Claim(CustomClaimTypes.DefaultPermission, $"{NAME}.{permission.ToLower()}"));
                }

                return claims;
            }
        }

        public static IList<string> ListClaimsAuthorizations { get { return ListAllClaims.Select(x => x.Value.Value).ToList(); } }
    }

    public enum PERMISSIONS
    {
        Create,
        Read,
        Update,
        Delete,
        Index
    }
}
