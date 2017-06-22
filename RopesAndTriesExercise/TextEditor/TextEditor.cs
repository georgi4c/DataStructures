using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace TextEditor
{
    class TextEditor : ITextEditor
    {
        private Trie<BigList<char>> usersStrings;
        private Trie<Stack<string>> usersStack;

        public TextEditor()
        {
            this.usersStrings = new Trie<BigList<char>>();
            this.usersStack = new Trie<Stack<string>>();
        }


        public void Login(string username)
        {
            this.usersStrings.Insert(username, new BigList<char>());
            this.usersStack.Insert(username, new Stack<string>());
        }

        public void Logout(string username)
        {
            this.usersStrings.Delete(username);
            this.usersStack.Delete(username);
        }
        public string Print(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return "";
            }
            return string.Join("", this.usersStrings.GetValue(username));
        }
        public void Prepend(string username, string str)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            this.usersStack.GetValue(username).Push(this.Print(username));
            this.usersStrings.GetValue(username).AddRangeToFront(str);
        }

        public void Clear(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            this.usersStack.GetValue(username).Push(this.Print(username));
            this.usersStrings.GetValue(username).Clear();
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }
            this.usersStack.GetValue(username).Push(this.Print(username));
            this.usersStrings.GetValue(username).RemoveRange(startIndex, length);
        }

        public void Insert(string username, int index, string str)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            this.usersStack.GetValue(username).Push(this.Print(username));
            this.usersStrings.GetValue(username).InsertRange(index, str);
        }

        public void Undo(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }
            var userString = this.usersStrings.GetValue(username);
            var userHistory = this.usersStack.GetValue(username);
            if (userHistory.Count == 0)
            {
                return;
            }
            var lastUserString = userHistory.Pop();
            userHistory.Push(this.Print(username));
            
            userString.Clear();
            userString.AddRangeToFront(lastUserString);
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }
            this.usersStack.GetValue(username).Push(this.Print(username));

            var userString = this.usersStrings.GetValue(username);
            var substr = this.usersStrings.GetValue(username).GetRange(startIndex, length);
            userString.Clear();
            userString.AddRangeToFront(substr);

        }
        
        public int Length(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return -1;
            }
            return this.usersStrings.GetValue(username).Count;
        }
        
        public IEnumerable<string> Users(string prefix = "")
        {
           return this.usersStrings.GetByPrefix(prefix);
        }
    }
}
