using IstanbulNs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Data
{
    public class IndexContext:DbContext
    {
        public IndexContext(DbContextOptions<IndexContext> options):base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Lang> Langs { get; set; }
        public DbSet<DoctorsInfo> DoctorsInfos { get; set; }
        public DbSet<OnlineQuery> OnlineQueries { get; set; }
        public DbSet<PhoneDoc> PhoneDocs { get; set; }
        public DbSet<ServiceInfo> ServiceInfo { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceName> ServiceNames { get; set; }
        public DbSet<ServicePictures> ServicePictures { get; set; }
        public DbSet<InfoStr> InfoStrs { get; set; }
        public DbSet<Pdf> PDFBase { get; set; }



        public DbSet<User> Users { get; set; }


        public DbSet<Home> Home { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OnlineQuery>()
                .HasOne(p => p.Doctor)
                .WithMany(b => b.OnlineQueries);
        }
    }
}
