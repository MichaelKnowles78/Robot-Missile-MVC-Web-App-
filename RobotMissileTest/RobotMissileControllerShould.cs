using NUnit.Framework;
using RobotMissile.Models;
using RobotMissile.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using Microsoft.EntityFrameworkCore;

namespace RobotMissileTest
{
    [TestFixture]
    [Category("Controllers")]
    class RobotMissileControllerShould
    {

        private GameStateContext _context;
        private RobotMissileController _controller;

        [SetUp]
        public void ConfigureContextAndController()
        {
            _context = new GameStateContext(new DbContextOptionsBuilder<GameStateContext>().UseInMemoryDatabase("GameState").Options);
            _controller = new RobotMissileController(_context);
        }

        [Test]
        public void RedirectOnNewGameToCurrentGame()
        {
            ActionResult res = _controller.NewGame();
            Assert.That(res, new RedirectsTo("CurrentGame"));
        }

        [Test]
        public void ReturnViewResultToIndexWhenGameStateExists()
        {
            GameState state = new GameState();
            _context.Add(state);
            _context.SaveChanges();

            ActionResult res = _controller.CurrentGame(state.GameStateId);
            Assert.That(res, new ReturnsView("Index"));
        }

        [Test]
        public void RedirectToNewGameWhenGameStateNotFound()
        {
            GameState state = new GameState();

            ActionResult res = _controller.CurrentGame(state.GameStateId);
            Assert.That(res, new RedirectsTo("NewGame"));
        }

        [Test]
        public void ReturnViewResultToIndexWhileGameHasNotEnded()
        {
            GameState state = new GameState();
            state.Code = 'A';
            _context.Add(state);
            _context.SaveChanges();

            ActionResult res = _controller.Play(state.GameStateId, "B");
            Assert.That(res, new ReturnsView("Index"));
        }

        [Test]
        public void RedirectToEndGameWhenGameHasEnded()
        {
            GameState state = new GameState();
            _context.Add(state);
            _context.SaveChanges();

            ActionResult res = _controller.Play(state.GameStateId, state.Code.ToString());
            Assert.That(res, new RedirectsTo("EndGame"));
        }

        [Test]
        public void ReturnViewResultToFinalWhenGameHasEnded()
        {
            GameState state = new GameState();
            _context.Add(state);
            _context.SaveChanges();

            ActionResult res = _controller.EndGame(state.GameStateId);
            Assert.That(res, new ReturnsView("Final"));
        }

        [Test]
        public void RemoveGameStateFromContextOnEndGame()
        {
            GameState state = new GameState();
            _context.Add(state);
            _context.SaveChanges();

            _controller.EndGame(state.GameStateId);
            Assert.That(_context.GameStates.Find(state.GameStateId), Is.Null);
        }

    }
}
