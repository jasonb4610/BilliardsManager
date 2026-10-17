import { useEffect, useState, type FormEvent } from 'react';
import { getPlayers, createPlayer, type Player } from './services/api';

function App() {
  const [players, setPlayers] = useState<Player[]>([]);
  const [loading, setLoading] = useState(true);
  const [newName, setNewName] = useState('');
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    getPlayers()
      .then(data => {
        setPlayers(Array.isArray(data) ? data : []);
        setLoading(false);
      })
      .catch(err => {
        console.error("Failed to fetch players", err);
        setLoading(false);
      });
  }, []);

  const handleAddPlayer = async (e: FormEvent) => {
    e.preventDefault();
    if (!newName.trim()) return;

    try {
      setSubmitting(true);
      const createdPlayer = await createPlayer(newName.trim());
      // Append the newly created player to our state array
      setPlayers(prev => [...prev, createdPlayer]);
      setNewName(''); // Clear input
    } catch (err) {
      console.error("Failed to create player", err);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div style={{ padding: '2rem', fontFamily: 'sans-serif', maxWidth: '600px', margin: '0 auto' }}>
      <h1>Billiards Tournament Manager</h1>

      {/* Add Player Form */}
      <div style={{ background: '#f4f4f4', padding: '1rem', borderRadius: '8px', marginBottom: '2rem' }}>
        <h3>Add New Player</h3>
        <form onSubmit={handleAddPlayer} style={{ display: 'flex', gap: '0.5rem' }}>
          <input
            type="text"
            placeholder="Player name..."
            value={newName}
            onChange={e => setNewName(e.target.value)}
            style={{ padding: '0.5rem', flex: 1, borderRadius: '4px', border: '1px solid #ccc' }}
          />
          <button 
            type="submit" 
            disabled={submitting}
            style={{ padding: '0.5rem 1rem', background: '#007bff', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}
          >
            {submitting ? 'Adding...' : 'Add Player'}
          </button>
        </form>
      </div>

      <h2>Player Roster</h2>
      {loading ? (
        <p>Loading players from .NET API...</p>
      ) : players.length === 0 ? (
        <p>No players found.</p>
      ) : (
        <ul>
          {players.map(player => (
            <li key={player.id} style={{ padding: '0.25rem 0' }}>{player.name}</li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default App;