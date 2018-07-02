using System;
using ATMMachine.Entities;

namespace ATMMachine.Interfaces
{
    public interface IRestockAtmInventory
    {
        IReadOnlyCashTransaction GetRestockAmount();
    }
}
