import axios from 'axios';

const API_BASE_URL = 'http://localhost:5283/api';

export interface Player {
    id: number;
    name: string;
    createdAt: string;
    updatedAt: string;
    ticketPurchases: any[];
}

export const getPlayers = async (): Promise<Player[]> => {
    const response = await axios.get(`${API_BASE_URL}/players`);
    return response.data;
};

export const createPlayer = async (name: string): Promise<Player> => { 
    const response = await axios.post(`${API_BASE_URL}/players`, { name });
    return response.data;
}
