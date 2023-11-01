import React from 'react'
import { createContext, useState, useEffect } from 'react'
import {
  GetBattleStartAsync,
  GetGameStateAsync,
  GetAttackAsync,
  EquipItemAsync,
  GetEnterStoreAsync,
  GetReturnToTownAsync,
  SellItemAsync,
  BuyItemAsync,
} from '../services/GameService'
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

  const returnToTown = async () => {
    let currentState = await GetReturnToTownAsync()
    setCurrentGameState(currentState)
  }

  const attackEnemy = async () => {
    let currentState = await GetAttackAsync()
    setCurrentGameState(currentState)
  }

  //Kan nog göra om denna till getEquipment och ändra if satser för att minska repetition
  //så kan man ta bort metoden getEquipmentForSale som jag (ted) Gjorde nedanför
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

  const enterStore = async () => {
    let currentState = await GetEnterStoreAsync()
    setCurrentGameState(currentState)
  }

  const getEquipmentForSale = async () => {
    if (currentGameState && currentGameState.location.name === 'Shop') {
      await setCurrentItems(currentGameState.location.equipmentForSale)
    } else {
      console.log('Game state or store not available')
    }
  }

  const sellItem = async (index) => {
    let currentState = await SellItemAsync(index)
    setCurrentGameState(currentState)
  }

  const buyItem = async (index) => {
    let currentState = await BuyItemAsync(index)
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
        enterStore,
        getEquipmentForSale,
        returnToTown,
        sellItem,
        buyItem,
        currentItems,
      }}
    >
      {children}
    </GameContext.Provider>
  )
}

export default GameContextProvider
