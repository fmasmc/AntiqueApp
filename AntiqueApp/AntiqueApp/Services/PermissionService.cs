using System;
using System.Collections.Generic;
using System.Text;

namespace AntiqueApp.Services
{
    public class PermissionService
    {
        public static PermissionService Instance { get; } = new();

        private readonly HashSet<string> _currentPermissions = new();

        private PermissionService() { }

        public bool HasPermission(string permission)
        {
            return _currentPermissions.Contains(permission);
        }

        public void UpdateUserPermissions(IEnumerable<string> permissions)
        {
            _currentPermissions.Clear();
            foreach (var p in permissions)
            {
                _currentPermissions.Add(p);
            }
        }

        public void SetAdminPermissions()
        {
            _currentPermissions.Clear();
            _currentPermissions.Add("exhibits.manage");
            _currentPermissions.Add("orders.manage");
            _currentPermissions.Add("clients.manage");
            _currentPermissions.Add("loginhistory.view");
            _currentPermissions.Add("cart.view");
            _currentPermissions.Add("profile.view");
            _currentPermissions.Add("exhibits.add_to_cart");
        }

        public void SetUserPermissions()
        {
            _currentPermissions.Clear();
            _currentPermissions.Add("cart.view");
            _currentPermissions.Add("profile.view");
            _currentPermissions.Add("exhibits.add_to_cart");
        }
    }
}
