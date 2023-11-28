using Contact.Application.CQRS.Core;
using Contact.Application.Interfaces;
using Contact.Application.Models.Request;
using Contact.Application.Models.Response;
using Contact.Domain.DTOs;
using Contact.Domain.Entities;
using Contact.Domain.Enums;
using Contact.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Services
{
    public class UserContactService : IUserContactService
    {
        private readonly ApplicationDbContext _context;

        public UserContactService(ApplicationDbContext context)
          => _context = context;

        public async Task<ApiResult<CreateUserContactResponse>> CreateUserContact(CreateUserContactRequest model, int userId)
        {


            //get contact from database with userid and email
            var contact = await _context.UserContacts.FirstOrDefaultAsync(c => c.UserId == userId && c.Email == model.Email);


            //if there is contact like this  then error
            if (contact != null)
                return ApiResult<CreateUserContactResponse>.Error(ErrorCodes.USER_CONTACT_IS_ALREADY_EXISTS);


            //if not create contact
            contact = new UserContact(model.Name,
                                      model.Surname,
                                      model.Phone,
                                      model.Email,
                                      model.Address);

            //add userid to contact
            contact.ConnectToUser(userId);

            //save it to database
            await _context.UserContacts.AddAsync(contact);
            await _context.SaveChangesAsync();


            return ApiResult<CreateUserContactResponse>.OK(new CreateUserContactResponse
            {
                ContactId = contact.Id
            });

        }

        public async Task<ApiResult<DeleteUserContactResponse>> DeleteUserContact(int contactId, int userId)
        {
            //get contact from database with userid and phone number
            var contact = await _context.UserContacts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactId);


            //if there is not contact like this  then error
            if (contact == null)
                return ApiResult<DeleteUserContactResponse>.Error(ErrorCodes.USER_CONTACT_IS_NOT_EXISTS);

            //delete this contact and save
            _context.UserContacts.Remove(contact);
            await _context.SaveChangesAsync();

            return ApiResult<DeleteUserContactResponse>.OK(new DeleteUserContactResponse
            {
                ContactId = contactId
            });
        }

        public async Task<ApiResult<GetUserContactDetailByIdResponse>> GetUserContactDetailById(int contactId, int userId)
        {
            //get contact from database with userid and phone number
            var contact = await _context
                                    .UserContacts
                                        .Where(c => c.UserId == userId && c.Id == contactId)
                                      .Select(c => new UserContactDto
                                      {
                                          ContactId = c.Id,
                                          Email = c.Email,
                                          Surname = c.Surname,
                                          Name = c.Name,
                                          Phone = c.Phone,
                                          Address = c.Address
                                      }).FirstOrDefaultAsync();
            //if there is not contact like this  then error
            if (contact == null)
                return ApiResult<GetUserContactDetailByIdResponse>.Error(ErrorCodes.USER_CONTACT_IS_NOT_EXISTS);

            //if there is then return it
            var response = new GetUserContactDetailByIdResponse
            {
                Contact = contact
            };

            return ApiResult<GetUserContactDetailByIdResponse>.OK(response);
        }

        public async Task<ApiResult<GetUserContactResponse>> GetUserContacts(GetUserContactRequest model, int userId)
        {

            //get user by userId from database
            var user = await _context
                                .Users
                                    .FirstOrDefaultAsync(c => c.Id == userId);


            //if there is not a user like this then error
            if (user == null)
                return ApiResult<GetUserContactResponse>.Error(ErrorCodes.USER_IS_NOT_EXISTS);


            //start IQueryable object with WHERE filter 
            var query = _context
                                    .UserContacts
                                        .Where(c =>

                    c.UserId == userId

                                        &&
                   (String.IsNullOrEmpty(model.Name) || (c.Name.StartsWith(model.Name) || c.Name.Contains(model.Name)))

                                        &&
                  (String.IsNullOrEmpty(model.Surname) || (c.Surname.StartsWith(model.Surname) || c.Surname.Contains(model.Surname)))

                                         &&
                 (String.IsNullOrEmpty(model.Email) || (c.Email.StartsWith(model.Email) || c.Email.Contains(model.Email)))
                                        &&

                 (String.IsNullOrEmpty(model.Phone) || (c.Phone.StartsWith(model.Phone) || c.Phone.Contains(model.Phone)))
                                        );


            //in order to get count of filtered data make countQuery variable
            var countQuery = query;

            //get count of filtered data
            int count = await countQuery.CountAsync();

            //check sorting requirement and sort it
            if (model.SortById == (byte)SortBy.Date)
                query = query.OrderByDescending(c => c.Created);
            else if (model.SortById == (byte)SortBy.Alphabetic)
                query = query.OrderByDescending(c => c.Name);

            //take some data
            if (model.Take != null)
                query = query.Skip((model.Page - 1) * (int)model.Take).Take((int)model.Take);


            //map it to dto and tolist it
            var contacts = await query.Select(c => new UserContactDto
            {
                ContactId = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            }).ToListAsync();

            var response = new GetUserContactResponse
            {
                Contacts = contacts,
                Take = model.Take,
                Page = model.Page,
                TotalCount = count
            };


            return ApiResult<GetUserContactResponse>.OK(response);
        }

        public async Task<ApiResult<UpdateUserContactResponse>> UpdateUserContact(UpdateUserContactRequest model, int contactId, int userId)
        {

            //get contact from database with userid and phone number
            var contact = await _context.UserContacts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == contactId);

            //if there is not contact like this  then error
            if (contact == null)
                return ApiResult<UpdateUserContactResponse>.Error(ErrorCodes.USER_CONTACT_IS_NOT_EXISTS);


            //update data
            contact.Name = model.Name;
            contact.Surname = model.Surname;
            contact.Email = model.Email;
            contact.Phone = model.Phone;
            contact.Address = model.Address;
            contact.Updated = DateTime.Now;

            //save it
            await _context.SaveChangesAsync();

            return ApiResult<UpdateUserContactResponse>.OK(new UpdateUserContactResponse
            {
                ContactId = contact.Id
            });

        }
    }
}
