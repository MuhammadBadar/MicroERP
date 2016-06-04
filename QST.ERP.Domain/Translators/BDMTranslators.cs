using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.AlKhair;
using QST.ERP.Domain.GroceryKit;
using QST.ERP.Domain.BDM;
using AutoMapper;

namespace QST.ERP.Domain.Translators
{
    public static class BDMTranslators
    {
        public static EntityDE ToEntity(this BankBE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BankBE, EntityDE>());
            EntityDE dest = Mapper.Map<EntityDE>(src);

            return dest;
        }

        public static BankBE ToBank(this EntityDE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EntityDE, BankBE>());
            BankBE dest = Mapper.Map<BankBE>(src);

            return dest;
        }

        public static BankBE ToBank(this EntityView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EntityView, BankBE>());
            BankBE dest = Mapper.Map<BankBE>(src);

            return dest;
        }

        public static Person ToPerson(this MemberDE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<MemberDE, Person>());
            Person dest = Mapper.Map<Person>(src);

            return dest;
        }
        public static EntityDE ToEntity(this Person src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Person, EntityDE>());
            EntityDE dest = Mapper.Map<EntityDE>(src);

            return dest;
        }

        public static Person ToPerson(this EntityDE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EntityDE, Person>());
            Person dest = Mapper.Map<Person>(src);

            return dest;
        }

        public static Person ToPerson(this PersonView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PersonView, Person>());
            Person dest = Mapper.Map<Person>(src);

            return dest;
        }

        public static EmployeeCoreBE ToEmployee(this Person src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Person, EmployeeCoreBE>());
            EmployeeCoreBE dest = Mapper.Map<EmployeeCoreBE>(src);

            return dest;
        }
        public static Person ToPerson(this EmployeeCoreBE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EmployeeCoreBE, Person>());
            Person dest = Mapper.Map<Person>(src);

            return dest;
        }

        public static EmployeeCoreBE ToEmployee(this DonorView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DonorView, EmployeeCoreBE>());
            EmployeeCoreBE dest = Mapper.Map<EmployeeCoreBE>(src);

            return dest;
        }

        public static EmployeeCoreBE ToEmployee(this PersonView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PersonView, EmployeeCoreBE>());
            EmployeeCoreBE dest = Mapper.Map<EmployeeCoreBE>(src);

            return dest;
        }

    }
}
