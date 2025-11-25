using Microsoft.Identity.Client;
using Moq;
using ProjectManager.BLL.Services;
using ProjectManager.DAL.Repositories.IRepositories;
using ProjectManager.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using Xunit;


namespace ProjectManager.Tests
{
    public class ProjectServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProjectRepository> _mockProjectRepo;

        private readonly ProjectService _service;

        public ProjectServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProjectRepo = new Mock<IProjectRepository>();

            _mockUnitOfWork.Setup(u => u.Projects).Returns(_mockProjectRepo.Object);


            _service = new ProjectService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnFalse_WhenUserIsNotOwner()
        {
            int projectId = 1;
            string ownerId = "UserA";
            string hackerId = "UserB";

            var existingProject = new Project
            {
                Id = projectId,
                Title = "Test Project",
                OwnerId = ownerId
            };

            _mockProjectRepo.Setup(repo => repo.GetAsync(projectId))
                .ReturnsAsync(existingProject);

            var result = await _service.DeleteProjectAsync(projectId, hackerId);


            Assert.False(result);

            _mockProjectRepo.Verify(repo => repo.Remove(It.IsAny<Project>()), Times.Never);

            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteProject_ShouldReturnTrue_WhenUserIsOwner()
        {
            int projectId = 1;
            string ownerId = "UserA";

            var existingProject = new Project
            {
                Id = projectId,
                OwnerId = ownerId
            };

            _mockProjectRepo.Setup(repo => repo.GetAsync(projectId))
                .ReturnsAsync(existingProject);

            var result = await _service.DeleteProjectAsync(projectId, ownerId);

            Assert.True(result);

            _mockProjectRepo.Verify(repo => repo.Remove(existingProject), Times.Once);

            _mockUnitOfWork.Verify(repo => repo.CompleteAsync(), Times.Once);
        } 
    }
}