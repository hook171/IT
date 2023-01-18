using domain.Models;
using BD.Models;

namespace BD.Convert
{
    public static class SpecializationConvert
    {
        public static Specialization? ToDomain(this SpecializationModel entity)
        {
            return new Specialization(

                entity.Id,
                entity.Name);
        }

        public static SpecializationModel ToModel(this Specialization entity)
        {
            return new SpecializationModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}