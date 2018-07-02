using System;
using ATMMachine.Entities;

namespace ATMMachine.interfaces
{
    public interface IRestockAtmInventory
    {
        IReadOnlyCashTransaction GetRestockAmount();
    }
}
