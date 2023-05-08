using Microsoft.EntityFrameworkCore;

namespace SupportTicketSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> User => Set<User>();
        public DbSet<Ticket> Ticket => Set<Ticket>();
        public DbSet<JoinUserTicket> JoinUserTicket => Set<JoinUserTicket>();
        public DbSet<Conversation> Conversation => Set<Conversation>();

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>().HasQueryFilter(x => x.Archived == null);
            modelbuilder.Entity<Ticket>().HasQueryFilter(x => x.Archived == null);
            modelbuilder.Entity<JoinUserTicket>().HasQueryFilter(x => x.Archived == null);
            modelbuilder.Entity<Conversation>().HasQueryFilter(x => x.Archived == null);

            modelbuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedBy)
                .WithMany(t => t.CreatedTickets)
                .HasForeignKey(t => t.CreatedByID)
                .OnDelete(DeleteBehavior.NoAction);

            modelbuilder.Entity<JoinUserTicket>()
                .HasOne(j => j.Ticket)
                .WithMany(j => j.InvolvedUsers)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            modelbuilder.Entity<JoinUserTicket>()
                .HasOne(j => j.InvolvedUser)
                .WithMany(j => j.JoinUserTicket)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelbuilder.Entity<Conversation>()
                .HasOne(c => c.FromUser)
                .WithMany(c => c.Conversations)
                .HasForeignKey(c => c.FromUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelbuilder.Entity<Conversation>()
                .HasOne(c => c.Ticket)
                .WithMany(c => c.Conversations)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
