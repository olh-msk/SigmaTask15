using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SigmaTask15.Models
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }

        //ми не хочемо, щоб наступне поле виводило у Json
        //бо це може спичинити рекурсивний цикл вічних виводів
        //коли місто виводить країну в якій воно є
        //а країна виводить всі міста, що має, і по колу міто виводить країну
        [JsonIgnore]
        public Country Country { get; set; }

        //не хочемо мати наступну властивість в базі даних
        [NotMapped]
        public string CountryName {
            get
            {
                return this.Country?.Name;
            }
        }
    }
}
