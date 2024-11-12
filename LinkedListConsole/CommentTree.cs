using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListsConsole
{
    class CommentNode
    {
        public string Text { get; set; }
        public List<CommentNode> Replies { get; set; }

        public CommentNode(string text)
        {
            Text = text;
            Replies = new List<CommentNode>();
        }
    }

    class CommentTree
    {
        private CommentNode root;

        public CommentTree()
        {
            root = null;
        }

        public void AddComment(string text, string parentText = null)
        {
            CommentNode newComment = new CommentNode(text);

            if (root == null)
            {
                root = newComment;
            }
            else
            {
                AddComment(root, newComment, parentText);
            }
        }

        private void AddComment(CommentNode current, CommentNode newComment, string parentText)
        {
            if (current.Text == parentText)
            {
                current.Replies.Add(newComment);
            }
            else
            {
                foreach (var reply in current.Replies)
                {
                    AddComment(reply, newComment, parentText);
                }
            }
        }

        public void DisplayComments()
        {
            if (root == null)
            {
                Console.WriteLine("Комментариев нет.");
            }
            else
            {
                DisplayComments(root, 0);
            }
        }

        private void DisplayComments(CommentNode node, int level)
        {
            Console.WriteLine(new string(' ', level * 2) + node.Text);
            foreach (var reply in node.Replies)
            {
                DisplayComments(reply, level + 1);
            }
        }

        public void RemoveComment(string text)
        {
            root = RemoveComment(root, text);
        }

        private CommentNode RemoveComment(CommentNode current, string text)
        {
            if (current == null)
            {
                return null;
            }

            if (current.Text == text)
            {
                return null;
            }

            current.Replies = current.Replies.ConvertAll(reply => RemoveComment(reply, text));
            return current;
        }
    }
}
