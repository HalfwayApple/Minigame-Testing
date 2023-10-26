import React from 'react'
import { useNavigate } from 'react-router-dom';
import './WelcomePage.css';  // Make sure to import the CSS file

const WelcomePage = () => {
    const navigate = useNavigate()

    const handleClick = () => {
        // Adding a timeout to create a delay for the transition effect
        setTimeout(() => {
            navigate('/PlayGame');
        }, 500);
    }

    return (
        <div className="welcome-container">
            <button className="button" onClick={() => handleClick()}>Start Game</button>
        </div>
    )
}

export default WelcomePage
