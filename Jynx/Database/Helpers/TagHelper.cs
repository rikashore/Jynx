using Jynx.Database.Entities;
using System.Threading.Tasks;

namespace Jynx.Database.Helpers
{
    public class TagHelper
    {
        public JynxContext JynxContext { get; set; }

        public TagHelper(JynxContext context)
        {
            JynxContext = context;
        }

        public async Task AddTag(string tagName, string content)
        {
            JynxContext.Add(new Tag { Name = tagName, Content = content });

            await JynxContext.SaveChangesAsync();
        }

        public async Task<Tag> GetTag(string tagName)
        {
            var tag = await JynxContext.Tags.FindAsync(tagName);

            if (tag == null)
                return null;

            return tag;
        }

        public async Task<int> DeleteTag(string tagName)
        {
            var tag = await JynxContext.Tags.FindAsync(tagName);

            if (tag == null)
                return 0;

            JynxContext.Tags.Remove(tag);

            await JynxContext.SaveChangesAsync();

            return 1;
        }
    }
}
