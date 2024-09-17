using System;

public class SpawnerBots : SpawnerObjects<Bot>
{
    public event Action<SpawnerBots, Bot> BotSpawned;

    public Bot SpawnBot(Base @base)
    {
        Bot bot = Get();
        bot.AssignBase(@base);
        bot.transform.position = @base.transform.position;

        return bot;
    }

    protected override void OnGet(Bot bot)
    {
        bot.Arrived += OnSpecifyArrivalBot;
        base.OnGet(bot);
    }

    protected override void OnRelease(Bot bot)
    {
        bot.Arrived -= OnSpecifyArrivalBot;
        base.OnRelease(bot);
    }

    protected override void Destroy(Bot bot)
    {
        bot.Arrived -= OnSpecifyArrivalBot;
        base.Destroy(bot);
    }

    private void OnSpecifyArrivalBot(Bot bot) =>
        BotSpawned?.Invoke(this, bot);
}
