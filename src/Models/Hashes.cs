using System.Text.Json.Serialization;
using TrueSecProject.Serialization; // We will create this converter next

namespace TrueSecProject.Models;

[JsonConverter(typeof(HashesConverter))]
public class Hashes : Dictionary<string, string>
{
    public Hashes() : base() { }

    public Hashes(IDictionary<string, string> dictionary) : base(dictionary) { }
}