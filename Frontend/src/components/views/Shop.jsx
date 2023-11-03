import React, { useContext } from 'react';
import { GameContext } from '../../contexts/GameContext';
import './Shop.css';


const Shop = () => {
    const { currentGameState, returnToTown, buyItem, sellItem, handleNavigateSite, currentShopItems} = useContext(GameContext);

    return (
        <div className="game-container">

        <div className="hero-card" id="hero-card">
                <img className="fighterImage" id="fighter-image" src="https://cdn.discordapp.com/attachments/1068620070227562659/1164550263479226449/imageedit_4_5377634979.gif?ex=65439ef3&is=653129f3&hm=0a989bec8fefb11f7c983f22dff0c21443ac03f6a8a5a4ca7d105d99c034b2d6&" alt="Hero" />
                <h3 id="hero-name">{currentGameState.hero.name} The Hero</h3>
                <p id="hero-level">Level: {currentGameState.hero.level}</p>
                <p id="hero-money">Money: {currentGameState.hero.money}</p>
                {currentGameState.hero.equippedWeapon ? (
                    <p id="hero-weapon">Weapon: {currentGameState.hero.equippedWeapon.name} + {currentGameState.hero.equippedWeapon.attackPower}</p>
                ) : (
                    <p id="hero-weapon">Weapon: Unarmed</p>
                )}
                {currentGameState.hero.equippedArmor ? (
                    <p id="hero-armor">Armor: {currentGameState.hero.equippedArmor.name} + {currentGameState.hero.equippedArmor.armorValue}</p>
                ) : (
                    <p id="hero-armor">Armor: Unarmored</p>
                )}
                <p id="hero-armor-value">Armor Value: {currentGameState.hero.armorValue}</p>
                <p id="hero-attack-power">Attack Power: {currentGameState.hero.attackPower}</p>
            </div>
            
        {currentGameState ? (
            <div className="shop-container" id="shop-container">
            <div>
                <h1 className="game-area-text" id="current-location">You are currently in: {currentGameState.location.name}</h1>
                <h2 id="shop-inventory-title">Shop Inventory</h2>
                <ul id="shop-text-display" className="action-flow">
                {currentShopItems.map(listItem => {
                    return <li key={currentShopItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                        <button id='buy-button' data-testid="buy-button-test"
                        onClick={() => {
                            buyItem(currentShopItems.indexOf(listItem));
                        }}>Buy</button></li>
                    })
                }
                </ul>

                <h2 id="player-inventory-title">Your Inventory</h2>
                <ul id="player-text-display" className="action-flow">
                    {currentGameState.hero.equipmentInBag.map((item, index) => (
                        <li key={index}>
                            {item.name} + {item.attackPower}{item.armorValue}
                            <button data-testid="sell-button-test" onClick={() => {
                                sellItem(index);
                                }}>Sell</button>
                        </li>
                    ))}
                </ul>
            </div>
            <button className="button" id="leave-button" data-testid="leave-button-test" onClick={() => {
                returnToTown();
                currentGameState.location.name = "Town";
                }}>Leave</button>
        </div>
            
        ) : (
            <p id="error-message">Error Loading game</p>
        )}
      
    </div>
  )
}

export default Shop;
