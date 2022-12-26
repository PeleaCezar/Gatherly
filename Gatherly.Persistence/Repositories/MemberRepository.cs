using Dapper;
using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories
{
    public sealed class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISqlConnectionFactory _connectionFactory;

        public MemberRepository(ApplicationDbContext dbContext, ISqlConnectionFactory connectionFactory)
        {
            _dbContext = dbContext;
            _connectionFactory = connectionFactory;
        }
            

        public async Task<Member> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(member => member.Id == id, cancellationToken);

        public async Task<Member> GetByIdWithDapperAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

            Member member = await sqlConnection
                 .QueryFirstOrDefaultAsync<Member>(
                 @"SELECT Id, Email, FirstName, LastName 
                   FROM Members
                   WHERE Id = @MemberId",
                  new
                  {
                     id
                  });

            return member;
        }
            
        public async Task<Member> GetByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
             await _dbContext
                .Set<Member>()
                .FirstOrDefaultAsync(member => member.Email == email, cancellationToken);

        public async Task<bool> IsEmailUniqueAsync(
            Email email,
            CancellationToken cancellationToken = default) =>
            !await _dbContext
                .Set<Member>()
                .AnyAsync(member => member.Email == email, cancellationToken);

        public void Add(Member member) =>
            _dbContext.Set<Member>().Add(member);

        public void Update(Member member) =>
            _dbContext.Set<Member>().Update(member);
    }
}
