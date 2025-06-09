using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Song
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Duration { get; set; }

    public Song(string title, string artist, int duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Artist: {Artist}, Duration: {Duration}s";
    }
}

class MusicDisk
{
    public string DiskName { get; set; }
    public ArrayList Songs { get; set; }

    public MusicDisk(string diskName)
    {
        DiskName = diskName;
        Songs = new ArrayList();
    }

    public void AddSong(Song song)
    {
        Songs.Add(song);
    }

    public void RemoveSong(string title)
    {
        Songs.Remove(Songs.Cast<Song>().FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase)));
    }
}

class MusicCatalog : Hashtable
{
    public void AddDisk(MusicDisk disk)
    {
        Add(disk.DiskName, disk);
    }

    public void RemoveDisk(string diskName)
    {
        Remove(diskName);
    }

    public void AddSongToDisk(string diskName, Song song)
    {
        if (ContainsKey(diskName))
            ((MusicDisk)this[diskName]).AddSong(song);
        else
            Console.WriteLine($"Disk {diskName} not found.");
    }

    public void RemoveSongFromDisk(string diskName, string songTitle)
    {
        if (ContainsKey(diskName))
            ((MusicDisk)this[diskName]).RemoveSong(songTitle);
        else
            Console.WriteLine($"Disk {diskName} not found.");
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("Music Catalog:");
        foreach (DictionaryEntry entry in this)
        {
            MusicDisk disk = (MusicDisk)entry.Value;
            Console.WriteLine($"Disk: {disk.DiskName}");
            foreach (Song song in disk.Songs)
                Console.WriteLine($"  {song}");
        }
    }

    public void DisplayDisk(string diskName)
    {
        if (ContainsKey(diskName))
        {
            MusicDisk disk = (MusicDisk)this[diskName];
            Console.WriteLine($"Disk: {disk.DiskName}");
            foreach (Song song in disk.Songs)
                Console.WriteLine($"  {song}");
        }
        else
        {
            Console.WriteLine($"Disk {diskName} not found.");
        }
    }

    public void SearchByArtist(string artist)
    {
        Console.WriteLine($"Songs by {artist}:");
        bool found = false;
        foreach (DictionaryEntry entry in this)
        {
            MusicDisk disk = (MusicDisk)entry.Value;
            foreach (Song song in disk.Songs)
            {
                if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Disk: {disk.DiskName}, {song}");
                    found = true;
                }
            }
        }
        if (!found)
            Console.WriteLine("No songs found.");
    }
}

