import React from "react";
import { render, screen } from "@testing-library/react";
import userEvent from '@testing-library/user-event';
import PlayGame from "./PlayGame";
import GameContextProvider, {GameContext} from "../../contexts/GameContext";
import { BrowserRouter } from 'react-router-dom'

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

describe('Playgame component', () => {
    it('renders the component without errors', () => {
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
});

it('should handle "Challenge an enemy" button click', () => {
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

    // Assert
    expect(mockEnterBattle).toHaveBeenCalled();
})

