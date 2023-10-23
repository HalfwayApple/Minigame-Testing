import { createContext, useState, useContext, useEffect } from "react";
import { GetBattleStartAsync, GetGameStateAsync, GetAttackAsync } from "../services/GameService";

export const GameContext = createContext();
export const GameContextProvider = ({ children }) => {
    const [currentGameState, setCurrentGameState] = useState();

    useEffect(() => {
        setGameState()
    }, []);

    const setGameState = async () => {
        let currentState = await GetGameStateAsync();
        setCurrentGameState(currentState);
    }

    const enterBattle = async () => {
        let currentState = await GetBattleStartAsync();
        setCurrentGameState(currentState);
    }

    const attackEnemy = async () => {
        let currentState = await GetAttackAsync();
        setCurrentGameState(currentState);
    }

    
    
    return (
        <GameContext.Provider value={{currentGameState, setCurrentGameState, enterBattle, attackEnemy}}>
            {children}
        </GameContext.Provider>
    );
};

export default GameContextProvider;