using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SigmaTask15.Models;

namespace SigmaTask15
{
    //Экземпляр DbContext представляет собой сочетание шаблонов
    //единиц работы и репозитория, которое можно использовать для
    //запроса из базы данных и группирования изменений, которые
    //затем будут записаны обратно в хранилище как единое целое.
    //DbContext концептуально напоминает ObjectContext.
    public class AppDBContext:DbContext
    {
        //DbSet<> то має бути унаслідувати DbContext
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }


        public AppDBContext():base()
        {

        }

        //Переопределите этот метод, чтобы настроить
        //базу данных (и другие параметры)
        //для использования в этом контексте.
        //Этот метод вызывается для каждого
        //экземпляра создаваемого контекста.
        //Базовая реализация ничего не делает.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Cities.db;");

        }

        //Переопределите этот метод для дальнейшей
        //настройки модели, обнаруженной соглашением
        //из типов сущностей, предоставленных в DbSet<TEntity>
        //свойствах производного контекста.
        //Полученная модель может быть кэширована и
        //повторно использоваться для последующих
        //экземпляров производного контекста.

        //при створені моделі ми хочемо, щоб підтягнувало
        //дані і коректно їх відображало
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //автоматично ( AutoInclude() ) додавати всі навігаційні властивості
            //заповнювати їх
            modelBuilder.Entity<Country>().Navigation(x => x.Cities).AutoInclude();
            modelBuilder.Entity<City>().Navigation(x => x.Country).AutoInclude();
        }
    }
}
