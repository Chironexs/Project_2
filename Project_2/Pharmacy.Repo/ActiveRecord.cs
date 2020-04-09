using System.Collections.Generic;

namespace Pharmacy.ActiveRecord
{
    public abstract class ActiveRecord<T>
    {
        public abstract void Open();
        public abstract T GetbyId(int id);
        public abstract List<T> Reload();
        public abstract int Save(T objct);
        public abstract void Remove(int id);
        public abstract void Dispose();
    }
}