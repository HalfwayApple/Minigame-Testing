import { createContext, useState, useEffect } from 'react'
import {
  GetBattleStartAsync,
  GetGameStateAsync,
  GetAttackAsync,
  EquipItemAsync,
  EnterShopAsync,
} from '../services/GameService'
console.log(EnterShopAsync)
export const GameContext = createContext()
export const GameContextProvider = ({ children }) => {
  const [currentGameState, setCurrentGameState] = useState()
  const [currentItems, setCurrentItems] = useState([])

  useEffect(() => {
    setGameState()
  }, [])

  useEffect(() => {
    getInventory()
  }, [currentGameState])

  const setGameState = async () => {
    let currentState = await GetGameStateAsync()
    setCurrentGameState(currentState)
  }

  const enterBattle = async () => {
    let currentState = await GetBattleStartAsync()
    setCurrentGameState(currentState)
  }

  const attackEnemy = async () => {
    let currentState = await GetAttackAsync()
    setCurrentGameState(currentState)
  }

  const getInventory = async () => {
    if (currentGameState && currentGameState.hero) {
      await setCurrentItems(currentGameState.hero.equipmentInBag)
    } else {
      console.log('Game state or hero not available')
    }
  }

  const equipItem = async (index) => {
    let currentState = await EquipItemAsync(index)
    setCurrentGameState(currentState)
  }

  const enterShop = async () => {
    let currentState = await EnterShopAsync()
    setCurrentGameState(currentState)
  }

  return (
    <GameContext.Provider
      value={{
        currentGameState,
        setCurrentGameState,
        enterBattle,
        attackEnemy,
        getInventory,
        equipItem,
        currentItems,
        enterShop,
      }}
    >
      {children}
    </GameContext.Provider>
  )
}

export default GameContextProvider
