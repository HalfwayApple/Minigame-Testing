import React from 'react';
import { render } from '@testing-library/react';
import { act } from 'react-dom/test-utils';
import GameContext, { GameContextProvider } from './GameContext';
import { BrowserRouter } from 'react-router-dom';
import PlayGame from '../components/views/PlayGame';
import { GetGameStateAsync } from '../services/GameService';
import { useContext } from 'react';

jest.mock('../services/GameService', () => ({
  GetGameStateAsync: jest.fn(),
  GetBattleStartAsync: jest.fn(),
  GetReturnToTownAsync: jest.fn(),
  GetAttackAsync: jest.fn(),
  EquipItemAsync: jest.fn(),
  GetEnterStoreAsync: jest.fn(),
  SellItemAsync: jest.fn(),
  BuyItemAsync: jest.fn(),
}));

//#region Mocked gamestate
const mockGameState = {
    "hero": {
        "xp": 10,
        "equippedWeapon": null,
        "equippedArmor": null,
        "equipmentInBag": [
            {
                "attackPower": 2,
                "name": "Sword",
                "price": 40
            }
        ],
        "money": 0,
        "id": 1,
        "name": "Ted",
        "description": null,
        "level": 1,
        "maxHP": 10,
        "currentHP": 10,
        "maxMana": 5,
        "currentMana": 5,
        "attackPower": 1,
        "armorValue": 0,
        "critChance": 0,
        "dodgeChance": 0
    },
    "enemyList": [
        {
            "noDropChance": 80,
            "xpValue": 5,
            "moneyValue": 10,
            "lootTable": [
                {
                    "attackPower": 2,
                    "name": "Sword",
                    "price": 40
                },
                {
                    "armorValue": 1,
                    "name": "Breastplate",
                    "price": 35
                }
            ],
            "id": 2,
            "name": "Slime",
            "description": "Japanese first enemy",
            "level": 1,
            "maxHP": 5,
            "currentHP": 5,
            "maxMana": 5,
            "currentMana": 0,
            "attackPower": 1,
            "armorValue": 0,
            "critChance": 0,
            "dodgeChance": 0
        },
        {
            "noDropChance": 80,
            "xpValue": 5,
            "moneyValue": 10,
            "lootTable": [
                {
                    "armorValue": 1,
                    "name": "Breastplate",
                    "price": 35
                }
            ],
            "id": 3,
            "name": "Rat",
            "description": "European first enemy",
            "level": 1,
            "maxHP": 3,
            "currentHP": 3,
            "maxMana": 3,
            "currentMana": 0,
            "attackPower": 2,
            "armorValue": 0,
            "critChance": 0,
            "dodgeChance": 0
        }
    ],
    "location": {
        "name": "Town"
    }
}
//#endregion

function TestConsumer() {
    const { currentGameState, setGameState } = useContext(GameContext);
  
    useEffect(() => {
      setGameState(mockGameState);
    }, [setGameState]);
  
    return (
      <div>
        {currentGameState && <div data-testid="hero-name">{currentGameState.hero.name}</div>}
      </div>
    );
  }

describe('GameContextProvider', () => {
    test('Should set the game state', async () => {
        GetGameStateAsync.mockResolvedValue(mockGameState);
        let contextValue;
        render(
            <BrowserRouter>
              <GameContextProvider value={{ currentGameState: mockGameState }}>
                {TestConsumer()}
              </GameContextProvider>
            </BrowserRouter>
          );

        await waitFor(() => {
        expect(screen.getByTestId('hero-name')).toHaveTextContent(mockGameState.hero.name);
        });
    })
});