using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.State
{
    public class CartStateService
    {
        public event Action? OnCartChanged;
        public void NotifyCartChanged() => OnCartChanged?.Invoke();
    }
}