/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BOSS1_DIE = 1900161504U;
        static const AkUniqueID BOSS2_DIE = 3692500719U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID MUSIC_BIOME1 = 3258538016U;
        static const AkUniqueID MUSIC_BIOME2 = 3258538019U;
        static const AkUniqueID MUSIC_COMBAT = 3944980085U;
        static const AkUniqueID MUSIC_EXPLORATION = 1078688352U;
        static const AkUniqueID MUSIC_GAMEPLAY = 620878633U;
        static const AkUniqueID MUSIC_TREE = 701652841U;
        static const AkUniqueID PLAY_CRYSTAL_AMB = 2720352881U;
        static const AkUniqueID PLAY_PLAYER_FOOTSTEPS = 98439365U;
        static const AkUniqueID TEST = 3157003241U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace BIOME1
        {
            static const AkUniqueID GROUP = 975021336U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID V1 = 1534528634U;
                static const AkUniqueID V2 = 1534528633U;
            } // namespace STATE
        } // namespace BIOME1

        namespace BIOME2
        {
            static const AkUniqueID GROUP = 975021339U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID V1_ = 1986569297U;
                static const AkUniqueID V2_ = 1969791524U;
            } // namespace STATE
        } // namespace BIOME2

        namespace MUSIC_EXPLORATION_STATES
        {
            static const AkUniqueID GROUP = 450540413U;

            namespace STATE
            {
                static const AkUniqueID BIOME1 = 975021336U;
                static const AkUniqueID BIOME2 = 975021339U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID TREE = 3322072369U;
            } // namespace STATE
        } // namespace MUSIC_EXPLORATION_STATES

        namespace MUSIC_GAMEPLAY_STATES
        {
            static const AkUniqueID GROUP = 4064220498U;

            namespace STATE
            {
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID EXPLORATION = 2582085496U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC_GAMEPLAY_STATES

        namespace MUSIC_STATES
        {
            static const AkUniqueID GROUP = 1690668539U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID TITLE = 3705726509U;
            } // namespace STATE
        } // namespace MUSIC_STATES

    } // namespace STATES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
