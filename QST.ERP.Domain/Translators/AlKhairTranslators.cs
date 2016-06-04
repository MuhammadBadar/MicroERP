using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.AlKhair;
using QST.ERP.Domain.BDM;
using AutoMapper;

namespace QST.ERP.Domain.Translators
{
    public static class AlKhairTranslators
    {
        public static DonorBE ToDonor(this Person src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Person, DonorBE>());
            DonorBE dest = Mapper.Map<DonorBE>(src);

            return dest;
        }
        public static Person ToPerson(this DonorBE src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DonorBE,Person>());
            Person dest = Mapper.Map<Person>(src);

            return dest;
        }

        public static DonorBE ToDonor(this DonorView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DonorView, DonorBE>());
            DonorBE dest = Mapper.Map<DonorBE>(src);

            return dest;
        }

        //public static List<DonorBE> ToDonors(this List<PersonView> src)
        //{
        //    Mapper.Initialize(cfg => cfg.CreateMap<DonorBE, Person>());
        //    List<DonorBE> list = new List<DonorBE>();
        //    foreach (var pv in src)
        //    {
        //        DonorBE dest = Mapper.Map<DonorBE>(pv);
        //        list.Add(dest);
        //    }

        //    return list;
        //}

        public static DonorBE ToDonor(this PersonView src)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PersonView, DonorBE>());
            DonorBE dest = Mapper.Map<DonorBE>(src);

            return dest;
        }

        //public static DonorView ToDonor(this PersonView src)
        //{
        //    Mapper.Initialize(cfg => cfg.CreateMap<PersonView, DonorView>());
        //    DonorView dest = Mapper.Map<DonorView>(src);

        //    return dest;
        //}
    }
}
