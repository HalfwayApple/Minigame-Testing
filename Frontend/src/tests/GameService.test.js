// import React from 'react';
// import { render, act, waitFor, screen } from '@testing-library/react';
// import fetchMock from 'jest-fetch-mock';
// import { GetGameStateAsync } from '../services/GameService';

// describe('GameService fetch tests', () => {

//     //#region MockGameState
//     const mockGameState = {
//     "hero": {
//         "xp": 10,
//         "equippedWeapon": null,
//         "equippedArmor": null,
//         "equipmentInBag": [
//             {
//                 "attackPower": 2,
//                 "name": "Sword",
//                 "price": 40
//             }
//         ],
//         "money": 0,
//         "id": 1,
//         "name": "Ted",
//         "description": null,
//         "level": 1,
//         "maxHP": 10,
//         "currentHP": 10,
//         "maxMana": 5,
//         "currentMana": 5,
//         "attackPower": 1,
//         "armorValue": 0,
//         "critChance": 0,
//         "dodgeChance": 0
//     },
//     "enemyList": [
//         {
//             "noDropChance": 80,
//             "xpValue": 5,
//             "moneyValue": 10,
//             "lootTable": [
//                 {
//                     "attackPower": 2,
//                     "name": "Sword",
//                     "price": 40
//                 },
//                 {
//                     "armorValue": 1,
//                     "name": "Breastplate",
//                     "price": 35
//                 }
//             ],
//             "id": 2,
//             "name": "Slime",
//             "description": "Japanese first enemy",
//             "level": 1,
//             "maxHP": 5,
//             "currentHP": 5,
//             "maxMana": 5,
//             "currentMana": 0,
//             "attackPower": 1,
//             "armorValue": 0,
//             "critChance": 0,
//             "dodgeChance": 0
//         },
//         {
//             "noDropChance": 80,
//             "xpValue": 5,
//             "moneyValue": 10,
//             "lootTable": [
//                 {
//                     "armorValue": 1,
//                     "name": "Breastplate",
//                     "price": 35
//                 }
//             ],
//             "id": 3,
//             "name": "Rat",
//             "description": "European first enemy",
//             "level": 1,
//             "maxHP": 3,
//             "currentHP": 3,
//             "maxMana": 3,
//             "currentMana": 0,
//             "attackPower": 2,
//             "armorValue": 0,
//             "critChance": 0,
//             "dodgeChance": 0
//         }
//     ],
//     "location": {
//         "name": "Town"
//     }
// }
// //#endregion

//     beforeAll(() => {
//         fetchMock.enableMocks(); // Enable fetch mocking
//     });
    
//     afterAll(() => {
//         fetchMock.disableMocks(); // Disable fetch mocking
//     });

//       test('should first', async () => {
//             fetchMock.mockResponseOnce(mockGameState);

//             const gameState = await GetGameStateAsync();

//             expect(gameState).toEqual(mockGameState);
//         });
// })