using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSnippetSaver.Models;

namespace CodeSnippetSaver.Controllers
{
    public class CodeController : Controller
    {
        private readonly CodeSnippetsDbContext _context;

        public CodeController(CodeSnippetsDbContext context)
        {
            _context = context;    
        }

        // GET: CodeSnippets
        public async Task<IActionResult> Index()
        {
            return View(await _context.CodeSnippets.ToListAsync());
        }

        // GET: CodeSnippets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.CodeSnippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // GET: CodeSnippets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CodeSnippets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text")] Code snippet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snippet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: CodeSnippets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.CodeSnippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }
            return View(snippet);
        }

        // POST: CodeSnippets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text")] Code snippet)
        {
            if (id != snippet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snippet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnippetExists(snippet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(snippet);
        }

        // GET: CodeSnippets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snippet = await _context.CodeSnippets.SingleOrDefaultAsync(m => m.ID == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: CodeSnippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var snippet = await _context.CodeSnippets.SingleOrDefaultAsync(m => m.ID == id);
            _context.CodeSnippets.Remove(snippet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SnippetExists(int id)
        {
            return _context.CodeSnippets.Any(e => e.ID == id);
        }
    }
}

