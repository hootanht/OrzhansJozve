using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Context
{
    public class OrzhansJozve_DbContext : IdentityDbContext
    {
        public OrzhansJozve_DbContext()
        {

        }
        public OrzhansJozve_DbContext(DbContextOptions<OrzhansJozve_DbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<MasterPageGroup> MasterPageGroups { get; set; }
        public virtual DbSet<PageGroup> PageGroups { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<NewsAgencyPeople> NewsAgencyPeople { get; set; }
        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<InsideAticelImage> InsideAticelImage { get; set; }
        public virtual DbSet<FooterLink> FooterLinks { get; set; }
    }
}
