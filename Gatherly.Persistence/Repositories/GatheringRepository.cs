﻿using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories
{
    public sealed class GatheringRepository : IGatheringRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GatheringRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Gathering>> GetByCreatorIdAsync(
            Guid creatorId,
            CancellationToken cancellationToken = default)
        {
            List<Gathering> gatherings = await _dbContext
                .Set<Gathering>()
                .Where(gathering => gathering.Creator.Id == creatorId)
                .ToListAsync(cancellationToken);

            return gatherings;
        }

        public async Task<Gathering> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            Gathering gathering = await _dbContext
                .Set<Gathering>()
                .AsSingleQuery()
                .Include(gathering => gathering.Creator)
                .Include(gathering => gathering.Attendees)
                .Include(gathering => gathering.Invitations)
                .IgnoreQueryFilters()
                .Where(x => x.Cancelled)
                .FirstOrDefaultAsync(
                    gathering => gathering.Id == id,
                    cancellationToken);

            return gathering;
        }
        public async Task<Gathering> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Gathering>()
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        }

        public async Task<Gathering> GetByIdWithInvitationsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Gathering>()
                .Include(x => x.Invitations)
                .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
        }
        public void Add(Gathering gathering)
        {
            _dbContext.Set<Gathering>().Add(gathering);
        }

        public void Remove(Gathering gathering)
        {
            _dbContext.Set<Gathering>().Remove(gathering);
        }
    }
}
