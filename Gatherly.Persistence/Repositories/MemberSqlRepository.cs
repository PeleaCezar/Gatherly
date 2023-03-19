using Dapper;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Gatherly.Persistence.Repositories
{
    public sealed class MemberSqlRepository : IMemberRepository
    {
        private readonly IDbConnection _dbConnection;
        public MemberSqlRepository(ApplicationDbContext dbContext) =>
            _dbConnection = dbContext.Database.GetDbConnection();

        public void Add(Member member)
        {
            MemberSnapshot snapshot = member.ToSnapshot();

            _dbConnection.Execute(
                @"INSERT INTO Members(Id, Email, FirstName, LastName)
              VALUES (@Id, @Email, @FirstName, @LastName)",
                snapshot);
        }

        public async Task<Member> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            MemberSnapshot memberSnapshot = await _dbConnection
                .QueryFirstOrDefaultAsync<MemberSnapshot>(
                    @"SELECT Id, Email, FirstName, LastName, CreatedOnUtc, ModifiedOnUtc
                  FROM Members
                  WHERE Email = @Email",
                    new { Email = email.Value });

            if (memberSnapshot is null)
            {
                return null;
            }

            return Member.FromSnapshot(memberSnapshot);
        }

        public async Task<Member> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            MemberSnapshot memberSnapshot = await _dbConnection
                .QueryFirstOrDefaultAsync<MemberSnapshot>(
                    @"SELECT Id, Email, FirstName, LastName, CreatedOnUtc, ModifiedOnUtc
                  FROM Members
                  WHERE Id = @MemberId",
                  new { MemberId = id });

            if (memberSnapshot is null)
            {
                return null;
            }

            return Member.FromSnapshot(memberSnapshot);
        }

        public Task<Member> GetByIdWithDapperAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetMembersAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Member member)
        {
            MemberSnapshot snapshot = member.ToSnapshot();

            _dbConnection.Execute(
                @"UPDATE Members
              SET Email = @Email, FirstName = @FirstName, LastName = @LastName
              WHERE Id = @Id",
                snapshot);
        }
    }
}
