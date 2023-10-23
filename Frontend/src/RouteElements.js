import React from 'react'
import { Routes, Route } from 'react-router-dom';
import WelcomePage from './components/views/WelcomePage';
import PlayGame from './components/views/PlayGame';

const RouteElements = () => {
  return (
    <Routes>
      <Route exact path='/' element={<WelcomePage/>} />
      <Route path='/PlayGame' element={<PlayGame />} />
    </Routes>
  )
}

export default RouteElements
