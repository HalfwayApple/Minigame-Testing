import React from "react";
import { render, screen, act } from "@testing-library/react";
import userEvent from '@testing-library/user-event';
import PlayGame from "./PlayGame";
import GameContextProvider, {GameContext} from "../../contexts/GameContext";
import { BrowserRouter, useNavigate } from 'react-router-dom'

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
//#region Mocked gamestateBattle
const mockGameStateBattle = {
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
        "enemy": {
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
        "damageDoneLastTurn": 0,
        "damageTakenLastTurn": 0,
        "name": "Battle"
    }
}
//#endregion


test('renders the component without errors', () => {
    // Arrange
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameState }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Assert
    expect(screen.getByText(/You are currently in:/)).toBeInTheDocument();
});

test('Should handle "Challenge an enemy" button click', () => {
    // Arrange
    const mockEnterBattle = jest.fn();
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameState, enterBattle: mockEnterBattle }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Act
    const challengeButton = screen.getByText('Challenge an enemy');
    userEvent.click(challengeButton);
    const ulElement = screen.getByTestId('text-display-test');
    const isEmpty = ulElement.textContent.trim() === ''; 

    // Assert
    expect(mockEnterBattle).toHaveBeenCalled();
    expect(isEmpty).toBe(true);
})

test('Should handle "Attack enemy" button click', () => {
    // Arrange
    const mockAttackEnemy = jest.fn();
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameStateBattle, attackEnemy: mockAttackEnemy }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Act
    const attackButton = screen.getByTestId('attack-button-test');
    userEvent.click(attackButton);

    // Assert
    expect(mockAttackEnemy).toHaveBeenCalled();
}) 

test('Should handle "Check inventory" button click', () => {
    // Arrange
    const mockDisplayInventory = jest.fn();
    const mockCurrentItems = [];
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameState, getInventory: mockDisplayInventory, currentItems: mockCurrentItems }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Act
    const displayButton = screen.getByTestId('checkinventory-button-test');
    act(() => {
        userEvent.click(displayButton);
    });
    const ulElement = screen.getByTestId('text-display-test');
    const isEmpty = ulElement.textContent.trim() === ''; 

    // Assert
    expect(mockDisplayInventory).toHaveBeenCalled();
    expect(isEmpty).toBeTruthy();
})

test('Should handle "Enter store" button click', () => {
    // Arrange
    const mockEnterStore = jest.fn();
    // const mockNavigate = jest.fn();
    // jest.mock('react-router-dom', () => ({
    //     ...jest.requireActual('react-router-dom'),
    //     useNavigate: () => mockNavigate,
    //   }));

    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameState, enterStore: mockEnterStore }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Act
    const storeButton = screen.getByTestId('enterStore-button-test');
    act(() => {
        userEvent.click(storeButton);
      });

    // Assert
    expect(mockEnterStore).toHaveBeenCalled();
    // expect(mockNavigate).toHaveBeenCalledWith('/Shop');
})

test('renders the hero card with correct values', () => {
    // Arrange
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameState }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Assert
    expect(screen.getByText(`${mockGameState.hero.name} The Hero`)).toBeInTheDocument();
    expect(screen.getByText(`Level: ${mockGameState.hero.level}`)).toBeInTheDocument();
    expect(screen.getByText(`Money: ${mockGameState.hero.money}`)).toBeInTheDocument();
    expect(screen.getByText(`HP: ${mockGameState.hero.currentHP}/${mockGameState.hero.maxHP}`)).toBeInTheDocument();
    expect(screen.getByText(`Mana: ${mockGameState.hero.currentMana}/${mockGameState.hero.maxMana}`)).toBeInTheDocument();
    expect(screen.getByText(`Attack Power: ${mockGameState.hero.attackPower}`)).toBeInTheDocument();
    expect(screen.getByText('Weapon: Unarmed')).toBeInTheDocument();
    expect(screen.getByText('Armor: Unarmored')).toBeInTheDocument();
});

test('renders the enemy card with correct values', () => {
    // Arrange
    render(
        <BrowserRouter>
            <GameContext.Provider value={{ currentGameState: mockGameStateBattle }}>
                <PlayGame />
            </GameContext.Provider>
        </BrowserRouter>
    );

    // Assert
    expect(screen.getByText(`${mockGameStateBattle.location.enemy.name}`)).toBeInTheDocument();
    expect(screen.getByText(`HP: ${mockGameStateBattle.location.enemy.currentHP}/${mockGameStateBattle.location.enemy.maxHP}`)).toBeInTheDocument();
    expect(screen.getByText(`Mana: ${mockGameStateBattle.location.enemy.currentMana}/${mockGameStateBattle.location.enemy.maxMana}`)).toBeInTheDocument();
    expect(screen.getByText(`Attack: ${mockGameStateBattle.location.enemy.attackPower}`)).toBeInTheDocument();
    expect(screen.getByText(`Armor: ${mockGameStateBattle.location.enemy.armorValue}`)).toBeInTheDocument();
});

