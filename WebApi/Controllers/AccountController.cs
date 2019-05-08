using System;
using System.Collections.Generic;
using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _repository.Account.FindAll();

                _logger.LogInfo($"Returned all accounts from database.");

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "AccountById")]
        public IActionResult GetAccountById(Guid id)
        {
            try
            {
                var accountById = _repository.Account.GetAccountById(id);

                if (accountById.IsObjectNull() || accountById.IsEmptyObject())
                {
                    _logger.LogError("Returned null.");
                    return NotFound();
                }

                return Ok(accountById);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{ownerId}")]
        public IActionResult CreateAccount(Guid ownerId, [FromBody] Account account)
        {
            try
            {
                var ownerById = _repository.Owner.GetOwnerById(ownerId);
                if (ownerById.IsEmptyObject() || ownerById.IsObjectNull())
                {
                    _logger.LogError("Given owner not found!");
                    return NotFound();
                }

                account.OwnerId = ownerById.Id;
                _repository.Account.Create(account);
                _logger.LogInfo("Account created!");
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{ownerId}")]
        public IActionResult UpdateAccount(Guid ownerId, [FromBody] Account account)
        {
            try
            {
                var ownerById = _repository.Owner.GetOwnerById(ownerId);
                if (ownerById.IsEmptyObject() || ownerById.IsObjectNull())
                {
                    _logger.LogError("Given owner not found!");
                    return NotFound();
                }

                var accountById = _repository.Account.GetAccountById(account.Id);
                
                if (accountById.IsEmptyObject() || accountById.IsObjectNull())
                {
                    _logger.LogError("Given account not found!");
                    return NotFound();
                }
                
                _repository.Account.UpdateAccount(accountById,account);
                _logger.LogInfo("Update successful!");
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{ownerId}/{accountId}")]
        public IActionResult DeleteAccount(Guid ownerId, Guid accountId)
        {
            var ownerById = _repository.Owner.GetOwnerById(ownerId);
            if (ownerById.IsEmptyObject() || ownerById.IsObjectNull())
            {
                _logger.LogError("Given owner not found!");
                return NotFound();
            }

            var accountById = _repository.Account.GetAccountById(accountId);
            
            if (accountById.IsEmptyObject() || accountById.IsObjectNull())
            {
                _logger.LogError("Given account not found!");
                return NotFound();
            }
            
            _repository.Account.DeleteAccount(accountById);
            _logger.LogInfo("Account deleted!");
            return NoContent();
        }
    }
}