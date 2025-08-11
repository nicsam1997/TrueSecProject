using System.Text.Json.Serialization;
using TrueSecProject.Serialization;

namespace TrueSecProject.Models;

[JsonConverter(typeof(HashesConverter))]
public class Hashes : Dictionary<string, string>
{
    public Hashes() : base() { }

    public Hashes(IDictionary<string, string> dictionary) : base(dictionary) { }
}