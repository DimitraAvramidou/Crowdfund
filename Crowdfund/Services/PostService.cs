﻿using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.CreateOptions;
using Crowdfund.Services.Interfaces;
using Crowdfund.Services.UpdateOptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crowdfund.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;
        public PostService(DataContext context)
        {
            _context = context;
        }

        public Post CreatePost(CreatePostOptions options)
        {
            if (options == null || string.IsNullOrWhiteSpace(options.Title) || string.IsNullOrWhiteSpace(options.Text))
            {
                return null;
            }
            var post = new Post()
            {
                Title = options.Title,
                Text = options.Text
            };
            
            _context.Add(post);
            return _context.SaveChanges() > 0 ? post : null;
        }

        public Post UpdatePost(Post postToUpdate, UpdatePostOptions options)
        {
            
            if (!string.IsNullOrWhiteSpace(options.Text))
            {
                postToUpdate.Text = options.Text;
            }
            
            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                postToUpdate.Title = options.Title;
            }

            return postToUpdate;
        }

        public Post GetPostById(int? id)
        {
            return id==null ? null : _context.Set<Post>().Find(id);
        }

        public bool DeletePost(Post postToDelete)
        {
            _context.Remove(postToDelete);
            return _context.SaveChanges() > 0;
        }
    }
}