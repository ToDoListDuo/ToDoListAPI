using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListAPI.Model;
using TodoListAPI.Model.Context;
using Microsoft.AspNetCore.Cors;

namespace TodoListAPI.Controllers
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : Controller
    {

        private MySQLContext _context;

        public ItemController(MySQLContext context)
        {
            _context = context; 
        }
        [HttpGet]
        [Route("findall")]
        public List<Todolist> Get()
        {
            return _context.Todolist.ToList();
        }

        [HttpPost]
        [Route("create")]
        public Todolist Create(Todolist list)
        {
            try
            {
                _context.Todolist.Add(list);
                _context.SaveChanges();
                return list;
            }
            catch(Exception e)
            {
                throw new Exception("Erro ao criar tarefa", e);
            }
        }

        [HttpPut]
        [Route("update")]
        public Todolist Update(Todolist list)
        {
            if (!Exists(list.Id)) return null;

            var result = _context.Todolist.FirstOrDefault(x => x.Id == list.Id);

            if (result != null)
            {
                try
                {
                    var update = _context.Todolist.Where(x => x.Id == list.Id).First();
                    update.Data = list.Data;
                    update.Finalizada = list.Finalizada;
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("Erro ao atualizar tarefa", e);
                }
            }

            return result;
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public void Delete(int id)
        {
            var result = _context.Todolist.SingleOrDefault(x => x.Id == id);

            if (result != null)
            {
                try
                {
                    _context.Todolist.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {

                    throw new Exception("Erro ao deletar tarefa ", e);
                }

            }
        }

        public bool Exists(long id)
        {
            return _context.Todolist.Any(x => x.Id == id);

        }


    }
}
