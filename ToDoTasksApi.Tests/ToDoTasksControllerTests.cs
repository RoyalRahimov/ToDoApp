using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoTasksApi.Controllers;
using ToDoTasksApi.Data;
using ToDoTasksApi.Models;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq; // Add this to enable Count() and other LINQ extension methods

namespace ToDoTasksApi.Tests;

public class ToDoTasksControllerTests
{
    private readonly ToDoContext _context;
    private readonly ToDoTasksController _controller;

    public ToDoTasksControllerTests()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(databaseName: "TasksDatabase")
            .Options;

        _context = new ToDoContext(options);
        _controller = new ToDoTasksController(_context);
    }

    [Fact]
    public async Task GetAllTasksTest()
    {
        // Arrange
        var tasks = new List<ToDoTask>
            {
                new ToDoTask { Id = 1, Title = "Task1", Description="some notes", IsDone=false, TaskPriorityLevel=new PriorityLevel
                {
                    PriorityName="Low", PriorityColor = "Green"
                }},
                new ToDoTask { Id = 2, Title = "Task2", Description="test notes", IsDone=true, TaskPriorityLevel=new PriorityLevel
                {
                    PriorityName="Medium", PriorityColor = "Yellow"
                }},
                new ToDoTask { Id = 3, Title = "Task3", Description="notes...", IsDone=false, TaskPriorityLevel=new PriorityLevel
                {
                    PriorityName="High", PriorityColor = "Red"
                }}
            };

        _context.ToDoTasks.AddRange(tasks);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ToDoTask>>>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<ToDoTask>>(actionResult.Value);
        Assert.Equal(3, returnValue.Count());
    }

    [Fact]
    public async Task GetTaskTest()
    {
        // Arrange
        var task = new ToDoTask
        {
            Id = 4,
            Title = "Task4",
            Description = "some notes",
            IsDone = false,
            TaskPriorityLevel = new PriorityLevel
            {
                PriorityName = "Low",
                PriorityColor = "Green"
            }
        };
        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();

        // Act
        var result = await _controller.GetToDoTask(4);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ToDoTask>>(result);
        var returnValue = Assert.IsType<ToDoTask>(actionResult.Value);
        Assert.Equal(task.Id, returnValue.Id);
        Assert.Equal(task.Title, returnValue.Title);
    }

    [Fact]
    public async Task CreateTaskTest()
    {
        // Arrange
        var task = new ToDoTask
        {
            Id = 5,
            Title = "Task5",
            Description = "some notes",
            IsDone = false,
            TaskPriorityLevel = new PriorityLevel
            {
                PriorityName = "Low",
                PriorityColor = "Green"
            }
        };

        // Act
        var result = await _controller.PostToDoTask(task);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result); // Extract the CreatedAtActionResult
        var createdTask = Assert.IsType<ToDoTask>(createdAtActionResult.Value); // Extract the ToDoTask from the result
        Assert.NotNull(createdTask);
        Assert.Equal(task.Id, createdTask.Id);
        Assert.Equal(task.Title, createdTask.Title);
    }

    [Fact]
    public async Task UpdateTaskTest()
    {
        // Arrange
        var task = new ToDoTask
        {
            Id = 6,
            Title = "Task6",
            Description = "some notes",
            IsDone = false,
            TaskPriorityLevel = new PriorityLevel
            {
                PriorityName = "Low",
                PriorityColor = "Green"
            }
        };
        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();

        task.Title = "Task666";
        task.TaskPriorityLevel = new PriorityLevel
        {
            PriorityName = "High",
            PriorityColor = "Red"
        };

        // Act
        var result = await _controller.PutToDoTask(6, task);

        // Assert
        var updatedTask = await _context.ToDoTasks.FindAsync(6);
        Assert.Equal("Task666", updatedTask.Title);
        Assert.Equal("Red", updatedTask.TaskPriorityLevel.PriorityColor);
    }

    [Fact]
    public async Task DeleteTaskTest()
    {
        // Arrange
        var task = new ToDoTask
        {
            Id = 7,
            Title = "Task7",
            Description = "test description",
            IsDone = false,
            TaskPriorityLevel = new PriorityLevel
            {
                PriorityName = "Low",
                PriorityColor = "Green"
            }
        };
        _context.ToDoTasks.Add(task);
        await _context.SaveChangesAsync();

        // Act
        _ = await _controller.DeleteToDoTask(7);

        // Assert
        var deletedTask = await _context.ToDoTasks.FindAsync(7);
        Assert.Null(deletedTask);
    }
}