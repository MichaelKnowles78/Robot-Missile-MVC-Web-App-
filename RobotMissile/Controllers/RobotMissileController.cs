using Microsoft.AspNetCore.Mvc;
using RobotMissile.Models;
using System;

namespace RobotMissile.Controllers;

[Route("RobotMissile")]
public class RobotMissileController : Controller
{
    private readonly GameStateContext _context;

    public RobotMissileController(GameStateContext context)
    {
        _context = context;
    }

    [Route("")]
    public ActionResult NewGame()
    {
        GameState state = new();
        _context.GameStates.Add(state);
        _context.SaveChanges();
        return RedirectToAction("CurrentGame", new { id = state.GameStateId });
    }

    [Route("Game/{id}")]
    public ActionResult CurrentGame(Guid id)
    {
        GameState state = _context.GameStates.Find(id);
        if (state != null)
        {
            return View("Index", state);
        }
        else
        {
            return RedirectToAction("NewGame");
        }
    }

    [Route("Game/{id}/{value}")]
    public ActionResult Play(Guid id, string value)
    {
        var state = _context.GameStates.Find(id);
        if (state != null)
        {
            state.ProcessGuess(Convert.ToChar(value));
            _context.SaveChanges();

            if (state.GameEnded)
            {
                return RedirectToAction("EndGame", new { id = state.GameStateId });
            }
            else
            {
                return View("Index", state);
            }
        }
        else
        {
            return RedirectToAction("NewGame");
        }
    }

    [Route("Game/{id}/Final")]
    public ActionResult EndGame(Guid id)
    {
        GameState state = _context.GameStates.Find(id);

        if (state != null)
        {
            _context.GameStates.Remove(state);
            _context.SaveChanges();

            return View("Final", state);
        }
        else
        {
            return RedirectToAction("NewGame");
        }
    }
}