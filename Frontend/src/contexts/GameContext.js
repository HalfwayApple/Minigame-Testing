import { createContext, useState, useEffect } from "react";
import { GetBattleStartAsync, GetGameStateAsync, GetAttackAsync, EquipItemAsync } from "../services/GameService";

export const GameContext = createContext();
export const GameContextProvider = ({ children }) => {
    const [currentGameState, setCurrentGameState] = useState();
    const [currentItems, setCurrentItems] = useState([]);

    useEffect(() => {
        setGameState();
    }, []);

    useEffect(() => {
        getInventory();
    }, [currentGameState]);

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

    const getInventory = async () => {
        await setCurrentItems(currentGameState.hero.equipmentInBag);
        console.log(currentGameState);
        console.log(currentItems);
    }

    const equipItem = async (index) => {
        let currentState = await EquipItemAsync(index);
        setCurrentGameState(currentState);
    }

    
    
    
    return (
        <GameContext.Provider value={{currentGameState, setCurrentGameState, enterBattle, attackEnemy, getInventory, equipItem, currentItems}}>
            {children}
        </GameContext.Provider>
    );
};

export default GameContextProvider;