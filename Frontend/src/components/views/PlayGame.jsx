import "./PlayGame.css";
import { React, useContext, useState } from "react";
import { GameContext } from "../../contexts/GameContext";
import { useNavigate } from "react-router-dom";

const PlayGame = () => {

    const {currentGameState, enterBattle, attackEnemy, getInventory, currentItems, equipItem, enterStore} = useContext(GameContext);

    const navigate = useNavigate();
    const [showItems, setShowItems] = useState(false);

    let enemyImage = "";

    if(currentGameState.location.name !== "Town"){
        if (currentGameState.location.enemy.name === "Slime"){
            enemyImage = "https://cdn.discordapp.com/attachments/1024651946721824768/1164949923725316096/slimepicfixxed.png?ex=65451329&is=65329e29&hm=e29ccf5814c4ff09a6d1ecf2285877fc2b8036f8d41062fe6b9e0a2e2053a0bd&"
        }else if(currentGameState.location.enemy.name === "Rat"){
            enemyImage = "https://i.pinimg.com/originals/2f/7c/78/2f7c78c4378b7aae8d237e083732884f.png"
        }
    }

    const handleClick = () => { navigate('/Shop'); }

    const displayInventory = () => {
        getInventory();
    }
    
  return (
    <div className="game-container">
        {currentGameState ? (
            <>
                <div className="hero-card">
                    <img className="fighterImage" src="https://cdn.discordapp.com/attachments/1068620070227562659/1164550263479226449/imageedit_4_5377634979.gif?ex=65439ef3&is=653129f3&hm=0a989bec8fefb11f7c983f22dff0c21443ac03f6a8a5a4ca7d105d99c034b2d6&" alt="Hero" />
                    <h3>{currentGameState.hero.name} The Hero</h3>
                    <p>Level: {currentGameState.hero.level}</p>
                    <p>HP: {currentGameState.hero.currentHP}/{currentGameState.hero.maxHP}</p>
                    <p>Mana: {currentGameState.hero.currentMana}/{currentGameState.hero.maxMana}</p>
                    {currentGameState.hero.equippedWeapon ? (
                        <p>Weapon: {currentGameState.hero.equippedWeapon.name} + {currentGameState.hero.equippedWeapon.attackPower}</p>
                    ) : (
                        <p>Weapon: Unarmed</p>
                    )}
                    {currentGameState.hero.equippedArmor ? (
                        <p>Armor: {currentGameState.hero.equippedArmor.name} + {currentGameState.hero.equippedArmor.armorValue}</p>
                    ) : (
                        <p>Armor: Unarmored</p>
                    )}
                    <p>Armor Value: {currentGameState.hero.armorValue}</p>
                    <p>Attack Power: {currentGameState.hero.attackPower}</p>
                    <p>Experience: {currentGameState.hero.xp}</p>
                </div>
            
                <div>
                    <h1 className="game-area-text">You are currently in: {currentGameState.location.name}</h1>
                    <ul id="text-display" className="action-flow">
                        {showItems === true && 
                            currentItems.map(listItem => {
                                return <li key={currentItems.indexOf(listItem)}>{listItem.name} + {listItem.attackPower}{listItem.armorValue} 
                                <button 
                                onClick={() => {
                                    equipItem(currentItems.indexOf(listItem));
                                    setShowItems(false);
                                }}>Equip</button></li>
                            })
                        }
                        
                    </ul>
                    <div className="button-container">
                        {currentGameState.location.name !== "Battle" &&
                            <button className="game-button" onClick={() =>{
                                enterBattle();
                                setShowItems(false);
                            }}>Challenge an enemy</button>
                        }
                        {currentGameState.location.name === "Battle" &&
                            <button className="game-button" onClick={() => attackEnemy()}>Attack</button>
                        }
                        {currentGameState.location.name !== "Battle" &&
                            <button className="game-button" onClick={() => {
                                displayInventory();
                                setShowItems(true);
                            }}>Check inventory</button>
                        }

                        {currentGameState.location.name !== "Battle" &&
                            <button className="game-button" onClick={() =>{
                                enterStore();
                                handleClick();
                            }}>Enter Store</button>
                        }
                        
                    </div>
                </div>
                
                {currentGameState.location.name !== "Town" &&
                    <div className="enemy-card">
                        <img className="fighterImage" src={enemyImage} alt="Enemy" />
                        <h3>{currentGameState.location.enemy.name}</h3>
                        <p>Level: {currentGameState.location.enemy.level}</p>
                        <p>HP: {currentGameState.location.enemy.currentHP}/{currentGameState.location.enemy.maxHP}</p>
                        <p>Mana: {currentGameState.location.enemy.currentMana}/{currentGameState.location.enemy.maxMana}</p>
                        <p>Armor: {currentGameState.location.enemy.armorValue}</p>
                        <p>Attack: {currentGameState.location.enemy.attackPower}</p>
                    </div>
                }
                
                
            </>
            
        ) : (
            <p>Error Loading game</p>
        )}
      
    </div>
  )
}

export default PlayGame
